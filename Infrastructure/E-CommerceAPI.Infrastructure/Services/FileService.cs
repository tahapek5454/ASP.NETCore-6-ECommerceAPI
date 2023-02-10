using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Infrastructure.Operation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {

        private int Counter { get; set; } = 1;

        // bize wwwroot un yolunu ve kontrolunu saglayacak
        readonly IWebHostEnvironment _webHostEnviroment;
        public FileService(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
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

        async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            
            string newFileName = await Task.Run<string>(async () =>
            {       
                string extenitons = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);

                if (!first)
                {
                    int idx = oldName.LastIndexOf('-'); 
                    string before = oldName.Substring(0, idx);
                    oldName = $"{before}-{Counter}";                
                }

                string newFileName = NameOperation.CharecterRegulatory(oldName) + extenitons;

                if (File.Exists($"{path}\\{newFileName}"))
                {
                    Counter = Counter + 1;
                    // ilk defa ya ben eklenti kesin kendim yapacagim 
                    if(first)
                        return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}-{Counter}{Path.GetExtension(newFileName)}", false);
                    // ben zaten eklenti eklemisim yukarda yani artik eklenti ekleme return de
                    return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}{Path.GetExtension(newFileName)}", false);
                }
                else
                {
                    return newFileName;
                }
            });

            return newFileName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            // webrootPath wwwroot u getiriyor direkt -> wwwroot/path
            string uploadPath = Path.Combine(_webHostEnviroment.WebRootPath, path);
            // path yoksa olustur
            if(!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // donecek degerleri tutacal
            List<(string fileName, string path)> datas = new List<(string fileName, string path)>();

            // tum sonuclarun dogrulunu kontrol edicez
            List<bool> results = new List<bool>();

            // Collection olarak gelen files'dan file lari çekiyoruz collection->single
            foreach (IFormFile file in files)
            {
                // uygun isimlendirme islemi
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                    
                // kayıt islemi
                bool isDone = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);

                //verileri ekleme
                results.Add(isDone);
                datas.Add((fileNewName,Path.Combine(path, fileNewName))); 

            }

            if (results.TrueForAll(x => x.Equals(true)))
                return datas;

            //todo eger false gelirse hata fırlatmalı

            return null;
            
        }
    }
}
