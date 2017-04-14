using System.IO;

namespace Fw.Mustache
{
    public static class Render
    {
        public static string StringToString(string template, object data)
        {
            return StringToString(template, data, null);
        }

        public static string StringToString(string template, object data, TemplateLocator templateLocator)
        {
            var reader = new StringReader(template);
            var writer = new StringWriter();
            Template(reader, data, writer, templateLocator);
            return writer.GetStringBuilder().ToString();
        }

        public static string FileToString(string templatePath, object data)
        {
#if !VERACODE
            var template = File.ReadAllText(templatePath);
            var templateLocator = GetTemplateLocator(templatePath);
            return StringToString(template, data, templateLocator.GetTemplate);
#else
            return string.Empty;
#endif
        }

        public static void StringToFile(string template, object data, string outputPath)
        {
#if !VERACODE
            var reader = new StringReader(template);
            using (var writer = File.CreateText(outputPath))
            {
                Template(reader, data, writer, null);
            }
#endif
        }

        public static void FileToFile(string templatePath, object data, string outputPath)
        {
#if !VERACODE
            var reader = new StringReader(File.ReadAllText(templatePath));
            var templateLocator = GetTemplateLocator(templatePath);
            using (var writer = File.CreateText(outputPath))
            {
                Template(reader, data, writer, templateLocator.GetTemplate);
            }
#endif
        }

        public static void Template(TextReader reader, object data, TextWriter writer, TemplateLocator templateLocator)
        {
            var template = new Template();
            template.Load(reader);
            template.Render(data, writer, templateLocator);
        }

        private static FileSystemTemplateLocator GetTemplateLocator(string templatePath)
        {
            string dir = Path.GetDirectoryName(templatePath);
            string ext = Path.GetExtension(templatePath);
            return new FileSystemTemplateLocator(ext, dir);
        }
    }
}