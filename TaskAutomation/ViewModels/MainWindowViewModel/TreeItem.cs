using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels
{
    public abstract class TreeItem : ViewModel
    {
        protected string _Name;
        public virtual string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        #region Объект 
        public abstract object Object { get; }

        #endregion

        #region Вложенные объекты 
        protected ObservableCollection<TreeItem> _Items = new();
        public ObservableCollection<TreeItem> Items { get => _Items; }

        #endregion
        protected virtual void Area_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Items));
        }
        protected virtual void Objects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Items));
        }
        protected virtual void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Items));
        }
        public void Update()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Items));
        }

        protected void SetObj<T>(NotifyCollectionChangedEventArgs e, Func<T, TreeItem> func) where T : BaseModel
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var Items = e.NewItems;
                foreach (var item in Items)
                    _Items.Add(func((T)item));
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var Items = e.OldItems;
                foreach (var item in Items)
                    _Items.Remove(_Items.Single(x => x.Object == item));
            }
        }

        protected void SetList(NotifyCollectionChangedEventArgs e, ObservableCollection<Parameter> items)
        {
            var newCount = items.Count;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var Items = e.NewItems;
                var countAdding = Items.Count;
                if (countAdding == newCount)
                {
                    _Items.Insert(0, new TreeItemMainList(items));
                }    
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (newCount == 0)
                    _Items.RemoveAt(0);
            }
        }

        protected virtual void InitializeItems()
        {
            _Items.Clear();
        }

        protected virtual void InitializeObj<T>(ObservableCollection<T> values, Func<T,TreeItem> func)
        {
            foreach (var item in values)
                _Items.Add(func(item));
        }

        protected virtual void InitializeList(ObservableCollection<Parameter> values)
        {
            if (values.Count != 0)
                _Items.Add(new TreeItemMainList(values));
        }
    }

    public class TreeItemTask : TreeItem
    {
        #region Объект 
        private readonly Models.Task _Object;
        public override object Object => _Object;

        #endregion

        public TreeItemTask(Models.Task task)
        {
            _Object = task;
            _Name = _Object.Name;
            _Object.Parameters.CollectionChanged += Parameters_CollectionChanged;
            _Object.Areas.CollectionChanged += Area_CollectionChanged;
            _Object.Objects.CollectionChanged += Objects_CollectionChanged;
        }

        protected override void Objects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetObj<Models.Object>(e, x => new TreeItemObject(x));
            base.Objects_CollectionChanged(sender, e);
        }
        protected override void Area_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetObj<Area>(e, x => new TreeItemArea(x));
            base .Area_CollectionChanged(sender, e);
        }
        protected override void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetList(e, _Object.Parameters);
            base.Parameters_CollectionChanged(sender, e);
        }
    }
    public class TreeItemArea : TreeItem
    {
        #region Объект 
        private readonly Area _Object;
        public override object Object => _Object;
        #endregion

        public TreeItemArea(Area area)
        {
            _Object = area;
            _Name = _Object.Name;
            InitializeItems();
            _Object.Parameters.CollectionChanged += Parameters_CollectionChanged;
            _Object.Objects.CollectionChanged += Objects_CollectionChanged;
        }

        protected override void Objects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetObj<Models.Object>(e, x => new TreeItemObject(x));
            base.Objects_CollectionChanged(sender, e);
        }

        protected override void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetList(e, _Object.Parameters);
            base.Parameters_CollectionChanged(sender, e);
        }
        protected override void InitializeItems()
        {
            base.InitializeItems();
            InitializeList(_Object.Parameters);
            InitializeObj(_Object.Objects,x=> new TreeItemObject(x));
        }
    }

    public class TreeItemObject : TreeItem
    {
        #region Объект 
        private readonly Models.Object _Object;
        public override object Object => _Object;
        #endregion

        public TreeItemObject(Models.Object obj)
        {
            _Object = obj;
            _Name = _Object.Name;
            InitializeItems();
            _Object.Parameters.CollectionChanged += Parameters_CollectionChanged;
        }
        protected override void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetObj<Parameter>(e, x => new TreeItemParameter(x));
            base.Parameters_CollectionChanged(sender, e);
        }

        protected override void InitializeItems()
        {
            base.InitializeItems();
            InitializeObj(_Object.Parameters, x => new TreeItemParameter(x));
        }
    }

    public class TreeItemParameter : TreeItem
    {
        #region Объект 
        private readonly Parameter _Object;
        public override object Object => _Object;
        #endregion

        public TreeItemParameter(Parameter parameter)
        {
            _Object = parameter;
            _Name = _Object.Name;
        }
    }

    public class TreeItemMainList : TreeItem
    {
        #region Объект 
        private readonly ObservableCollection<Parameter> _Object;
        public override object Object => _Object;
        #endregion

        public TreeItemMainList(ObservableCollection<Parameter> list)
        {
            _Object = list;
            _Name = "Параметры";
            InitializeItems();
            _Object.CollectionChanged += Parameters_CollectionChanged;
        }
        protected override void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetObj<Parameter>(e, x => new TreeItemParameter(x));
            base.Parameters_CollectionChanged(sender, e);
        }
        protected override void InitializeItems()
        {
            base.InitializeItems();
            InitializeObj(_Object,x => new TreeItemParameter(x));
        }
    }
}




