using Abp.Application.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;

namespace MyFirstABP.Authorization
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, User>
    {
        public FeatureValueStore(ICacheManager cacheManager, IRepository<TenantFeatureSetting, long> tenantFeatureRepository, IRepository<Tenant> tenantRepository, IRepository<EditionFeatureSetting, long> editionFeatureRepository, IFeatureManager featureManager, IUnitOfWorkManager unitOfWorkManager) 
            : base(cacheManager, tenantFeatureRepository, tenantRepository, editionFeatureRepository, featureManager, unitOfWorkManager)
        {
        }
    }
}
