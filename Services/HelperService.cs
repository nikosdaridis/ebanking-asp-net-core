using AutoMapper;
using eBanking.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace eBanking.Services
{
    public class HelperService(BankDbContext dbContext, IMapper mapper)
    {
        /// <summary>
        /// Checks if entity exists in database
        /// </summary>
        public bool EntityExists<TModel>(uint searchNumber) where TModel : class =>
            dbContext.Set<TModel>().Any(entity => EF.Property<uint>(entity, "Number") == searchNumber);

        /// <summary>
        /// Finds entity by property name and value
        /// </summary>
        public TModel? FindEntityByProperty<TModel>(string propertyName, object value) where TModel : class =>
            FindEntityByProperty<TModel, TModel>(propertyName, value);

        /// <summary>
        /// Finds entity by property name and value and maps it to ViewModel
        /// </summary>
        public TViewModel? FindEntityByProperty<TModel, TViewModel>(string propertyName, object value) where TModel : class =>
            dbContext.Set<TModel>().FirstOrDefault(e => EF.Property<object>(e, propertyName).Equals(value)) is TModel entity
            ? mapper.Map<TViewModel>(entity)
            : default;
    }
}
