using Abp.Application.Editions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace MyFirstABP
{
    public class EditionManager : AbpEditionManager
    {
        public EditionManager(IRepository<Edition> editionRepository, IAbpZeroFeatureValueStore featureValueStore) 
            : base(editionRepository, featureValueStore)
        {
        }
    }
}
