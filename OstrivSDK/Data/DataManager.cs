namespace OstrivSDK
{
    public static class DataManager
    {
        public static Data Load(string filename)
        {
            using var file = File.OpenRead(filename);
            using var reader = new BinaryReader(file);

            var dataFile = new Data().FromBytes(reader);

            return dataFile;
        }
        public static Data LoadFolder(string path)
        {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            var entries = new List<DataEntry>();
            foreach (var filename in files)
            {
                var data = File.ReadAllBytes(filename);
                entries.Add(new DataEntry
                {
                    Filename = Path.GetRelativePath(path, filename).Replace("\\", "/"),
                    Data = data,
                    Size = data.Length,
                });
            }

            return new Data
            {
                Entries = entries.ToArray(),
            };
        }

        public static Data Save(this Data data, string filename)
        {
            using var file = File.OpenWrite(filename);
            using var writer = new BinaryWriter(file);

            data.ToBytes(writer);

            return data;
        }
        public static Data SaveFolder(this Data data, string path)
        {
            foreach (var entry in data.Entries)
            {
                var dirname = Path.GetDirectoryName(Path.Combine(path, entry.Filename)) ?? throw new ArgumentException($"{path} combined with {entry.Filename} leads to root.");
                if (!Directory.Exists(dirname))
                    Directory.CreateDirectory(dirname);

                using var file = File.Create(Path.Combine(path, entry.Filename));
                file.Write(entry.Data);
            }
            return data;
        }
    }
}
