using System.Text.Json;

namespace Repository.Logic.Repos
{
    public abstract class Repository<TModel> : IDisposable
        where TModel : Models.ModelObject, Contracts.IIdentifyable
    {
        #region Fields
        private List<TModel> _modelList = new();
        #endregion Fields

        #region Properties
        protected virtual string FilePath { get; set; } = $"{typeof(TModel).Name}.json";
        #endregion Properties

        protected Repository()
        {
            Load(FilePath);
        }
        protected Repository(string filePath)
        {
            FilePath = filePath;
            Load(FilePath);
        }

        #region Create
        public abstract TModel Create();
        public virtual Task<TModel> CreateAsync() => Task.Run(() => Create());
        #endregion Create

        #region Get
        public virtual TModel? GetById(int id) => _modelList.FirstOrDefault(m => m.Id == id);
        public virtual Task<TModel?> GetByIdAsync(int id) => Task.Run(() => GetById(id)); 
        public virtual TModel[] GetAll()
        {
            return _modelList.Where(m => m is TModel)
                            .ToArray();
        }
        public virtual Task<TModel[]> GetAllAsync() => Task.Run(() => GetAll());
        #endregion Get

        public void Clear() => _modelList.Clear();

        #region Add
        public virtual void Add(TModel model)
        {
            if (_modelList.Any())
            {
                model.Id = _modelList.Max(m => m.Id) + 1;
            }
            else
            {
                model.Id = 1;
            }
            _modelList.Add(model);
        }
        public virtual Task AddAsync(TModel model)
        {
            return Task.Run(() => Add(model));
        }
        #endregion Add

        #region Update
        public virtual bool Update(TModel model)
        {
            var listModel = _modelList.FirstOrDefault(m => m.Id == model.Id);

            if (listModel != null)
            {
                _modelList.Remove(listModel);
                _modelList.Add(model);
            }
            return listModel != null;
        }
        public virtual Task<bool> UpdateAsync(TModel model)
        {
            return Task.Run(() => Update(model));
        }
        #endregion Update

        #region Delete
        public virtual void Delete(int id)
        {
            var listModel = _modelList.FirstOrDefault(m => m.Id == id);

            if (listModel != null)
            {
                _modelList.Remove(listModel);
            }
        }
        public virtual Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }
        #endregion Delete

        #region SaveChanges
        public virtual void SaveChanges()
        {
            Save(FilePath);
        }
        public virtual Task SaveChangesAsync()
        {
            return Task.Run(() => SaveChanges());
        }
        #endregion SaveChanges

        #region Load and save
        internal virtual void Save(string filePath)
        {
            var jsonData = JsonSerializer.Serialize<TModel[]>(_modelList.ToArray());

            File.WriteAllText(filePath, jsonData);
        }
        internal virtual void Load(string filePath)
        {
            _modelList.Clear();
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                var models = JsonSerializer.Deserialize<TModel[]>(jsonData);

                if (models != null)
                {
                    _modelList.AddRange(models);
                }
            }
        }
        #endregion Load and save

        #region Dispose
        public void Dispose()
        {
        }
        #endregion Dispose
    }
}
