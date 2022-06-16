using System.Collections.Generic;
using System.Linq;
using Translation.Models;
using Translation.Models.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFCoreCodeFirstSample.Models.DataManager
{
    public class FunTranslationManager : IDataRepository<FunTranslation>
    {
        readonly ApplicationContext _funTranslationContext;

        public FunTranslationManager (ApplicationContext context)
        {
            _funTranslationContext = context;
        }

        public IEnumerable<FunTranslation> GetAll()
        {
            return _funTranslationContext.FunTranslations.ToList();
        }

        public FunTranslation Get(long id)
        {
            return _funTranslationContext.FunTranslations
                  .FirstOrDefault(e => e.Id == id);
        }

        public void Add(FunTranslation entity)
        {
            _funTranslationContext.Add(entity);
            _funTranslationContext.SaveChanges();
        }

        public void Update(FunTranslation funTranslation, FunTranslation entity)
        {
            funTranslation.NormalText = entity.NormalText;
            funTranslation.TranslatedText = entity.TranslatedText;
            
            _funTranslationContext.SaveChanges();
        }

        public void Delete(FunTranslation funTranslation)
        {
            _funTranslationContext.FunTranslations.Remove(funTranslation);
            _funTranslationContext.SaveChanges();
        }
    }
}