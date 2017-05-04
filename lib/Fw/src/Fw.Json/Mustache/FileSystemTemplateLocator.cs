using System.IO;

namespace Fw.Mustache
{
    public class FileSystemTemplateLocator
    {
        private readonly string _extension;
        private readonly string _directory;

        public FileSystemTemplateLocator(string extension, string directory)
        {
            _extension = extension;
            _directory = directory;
        }

        public Template GetTemplate(string name)
        {
            string path = Path.Combine(_directory, name + _extension);

            if (File.Exists(path))
            {
#if !VERACODE
                string text = File.ReadAllText(path);
#else
                string text = string.Empty;
#endif
                var reader = new StringReader(text);

                var template = new Template();
                template.Load(reader);

                return template;
            }

            return null;
        }
    }
}