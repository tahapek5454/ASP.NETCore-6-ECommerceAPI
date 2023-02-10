using E_CommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {

        // bize wwwroot un yolunu ve kontrolunu saglayacak
        readonly IWebHostEnvironment _webHostEnviroment;
        public LocalStorage(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
        }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)     
            => File.Delete(Path.Combine(pathOrContainerName, fileName));
    

        public List<string> GetFiles(string pathOrContainerName)
        {
            DirectoryInfo directory = new DirectoryInfo(pathOrContainerName);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
            => File.Exists(Path.Combine(pathOrContainerName, fileName));


        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                //stream nesnesi gerekli islemleri içerekce sekilde olusturuldu
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                // filestram bilgilerini ve islevini kullanrak ilgili dosyayı kopyala
                await file.CopyToAsync(fileStream);
                // filestrami temizle
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            // webrootPath wwwroot u getiriyor direkt -> wwwroot/path
            string uploadPath = Path.Combine(_webHostEnviroment.WebRootPath, pathOrContainerName);
            // path yoksa olustur
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // donecek degerleri tutacal
            List<(string fileName, string path)> datas = new List<(string fileName, string path)>();

            // Collection olarak gelen files'dan file lari çekiyoruz collection->single
            foreach (IFormFile file in files)
            {
                // uygun isimlendirme islemi
                string fileNewName = file.FileName;

                // kayıt islemi
                _ = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);

                //verileri ekleme
                datas.Add((fileNewName, Path.Combine(pathOrContainerName, fileNewName)));

            }

            //todo eger false gelirse hata fırlatmalı

            return datas;

        }
    }
}
