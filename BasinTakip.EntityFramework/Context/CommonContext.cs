using BasinTakip.Core.Data;
using BasinTakip.Core.Dependecy;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.EntityFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlFunctions = System.Data.Entity.SqlServer.SqlFunctions;

namespace BasinTakip.EntityFramework.Context
{
    public partial class CommonContext : DbContext, IScoppedDependency
    {
        static CommonContext()
        {
            DbInterception.Add(new FtsInterceptor());
        }
        public CommonContext()
            :base("name=DefaultConnection")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<PersonResult>();
            modelBuilder.Ignore<EditionResult>();
            modelBuilder.Ignore<EventResult>();
            modelBuilder.Ignore<ContactRecordResult>();

            modelBuilder.Configurations.Add(new PressMemberConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ContactRecordConfiguration());
            modelBuilder.Configurations.Add(new DistrictConfiguration());
            modelBuilder.Configurations.Add(new EditionConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new FirmConfiguration());
            modelBuilder.Configurations.Add(new PickListCategoryConfiguration());
            modelBuilder.Configurations.Add(new PickListConfiguration());
            modelBuilder.Configurations.Add(new PressMemberLicenseConfiguration());
            modelBuilder.Configurations.Add(new VehicleConfiguration());
            modelBuilder.Configurations.Add(new EventSpecialConfiguration());
            modelBuilder.Configurations.Add(new SearchTableConfiguration());
            modelBuilder.Configurations.Add(new DataHistoryConfiguration());
            modelBuilder.Configurations.Add(new LoginLogConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
