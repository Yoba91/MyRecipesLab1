using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using MyRecipesLab1.DAL.Models;
using Realms;

namespace MyRecipesLab1.DAL
{
    public class RealmRepository<TDataModel> where TDataModel : RealmObject, new()
    {
        private Realm _realm;
        private IMapper _mapper;

        public RealmRepository()
        {
            //Realm.DeleteRealm(RealmConfiguration.DefaultConfiguration);
            _realm = Realm.GetInstance();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly())));
        }

        public List<TDataModel> FindByName(Expression<Func<TDataModel, bool>> expression)
        {
            var dboList = _realm.All<TDataModel>().Where(expression).ToList();
            var result = _mapper.Map<List<TDataModel>>(dboList);
            return result;
        }

        public TDataModel GetByIdOrNew(long id)
        {
            var result = GetById(id);
            if (result is null)
            {
                result = new TDataModel();
            }
            return result;
        }

        public TDataModel GetById(long id)
        {
            var dbo = _realm.Find<TDataModel>(id);
            var result = _mapper.Map<TDataModel>(dbo);
            return result;
        }

        public List<TDataModel> GetAll()
        {
            var dboList = _realm.All<TDataModel>().ToList();
            var result = _mapper.Map<List<TDataModel>>(dboList);
            return result;
        }

        public TDataModel Save(TDataModel dataModel)
        {
            var trans = _realm.BeginWrite();
            _realm.Add(dataModel);
            trans.Commit();
            return _mapper.Map<TDataModel>(dataModel);
        }

        public void Remove(TDataModel dataModel)
        {
            _realm.Remove(dataModel);
        }

        public void RemoveById(long id)
        {
            var trans = _realm.BeginWrite();
            var dbo = _realm.Find<TDataModel>(id);
            if(dbo != null)
            {
                _realm.Remove(dbo);
            }
            trans.Commit();
            _realm.Refresh();
        }
    }
}
