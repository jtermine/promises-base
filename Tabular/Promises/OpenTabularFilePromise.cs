using System.IO;
using Tabular.Workloads;
using Termine.Promises;
using Termine.Promises.Generics;
using Termine.Promises.NLogInstrumentation;

namespace Tabular.Promises
{
    public class OpenTabularFilePromise: Promise<OpenTabularFileWorkload>
    {
        public override void Init()
        {
            this.WithNLogInstrumentation();
            this.WithValidator("checkFileName", CheckFileName);
            this.WithExecutor("openFile", OpenFile);
        }

        public OpenTabularFilePromise WithFileName(string fileName)
        {
            Workload.FileName = fileName;
            return this;
        }

        private void OpenFile(OpenTabularFileWorkload openTabularFileWorkload)
        {
            Trace(new GenericEventMessage(2, "Open file trace completed."));
        }

        private void CheckFileName(OpenTabularFileWorkload openTabularFileWorkload)
        {
            if (string.IsNullOrEmpty(openTabularFileWorkload.FileName) || !File.Exists(openTabularFileWorkload.FileName))
                Abort(new FileDoesNotExist(openTabularFileWorkload.FileName));
        }

        private class FileDoesNotExist : GenericEventMessage
        {
            public FileDoesNotExist(string fileName)
            {
                EventId = 1;
                EventPublicMessage = "The file does not exist";
                EventPublicDetails = string.Format("The file attempted was : {0}", fileName);
                IsPublicMessage = true;
            }
        }
    }
}