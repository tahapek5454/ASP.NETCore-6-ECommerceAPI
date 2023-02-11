using E_CommerceAPI.Infrastructure.Operation;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        private int Counter { get; set; } = 1;

        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string path, string fileName, HasFile hasFileMethod, bool first = true)
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

                if (hasFileMethod(path, newFileName))
                {
                    Counter = Counter + 1;
                    // ilk defa ya ben eklenti kesin kendim yapacagim 
                    if (first)
                        return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}-{Counter}{Path.GetExtension(newFileName)}", hasFileMethod,  false);
                    // ben zaten eklenti eklemisim yukarda yani artik eklenti ekleme return de
                    return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}{Path.GetExtension(newFileName)}", hasFileMethod, false);
                }
                else
                {
                    return newFileName;
                }
            });

            return newFileName;
        }
    }
}
