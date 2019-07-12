using EcommerceSite.Database;
using EcommerceSite.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSite.Services
{
    public class ConfigurationsService
    {
        #region Singleton
        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationsService();
                return instance;
            }
        }
        private static ConfigurationsService instance { get; set; }

        private ConfigurationsService()
        {
        }

        #endregion

        public List<Config> GetConfigs()
        {
            using(var context = new ESContext())
            {
                return context.Configurations.ToList();
            }
        }
        public Config GetConfig(string Key)
        {
            using(var context = new ESContext())
            {
                return context.Configurations.Find( Key);
            }
        }
        public void SaveConfiguration(Config configuration)
        {
            using (var context = new ESContext())
            {
                
                context.Configurations.Add(configuration);
                context.SaveChanges();
            }
        }

        public void UpdateConfiguration(Config configuration)
        {
            using (var context = new ESContext())
            {
                context.Entry(configuration).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteConfiguration(string key)
        {
            using (var context = new ESContext())
            {
                var configuration = context.Configurations.Find(key);
                context.Entry(configuration).State = EntityState.Deleted;
                //context.Configurations.Remove(configuration);
                context.SaveChanges();
            }
        }

        public int PageSize()
        {
            using(var context = new ESContext())
            {
                var pageSizeConfig= context.Configurations.Find( "PageSize");
                return pageSizeConfig !=null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }

        public int ShopPageSize()
        {
            using(var context = new ESContext())
            {
                var pageSizeConfig= context.Configurations.Find( "PageSize");
                return pageSizeConfig !=null ? int.Parse(pageSizeConfig.Value) : 40;
            }
        }
    }
}
