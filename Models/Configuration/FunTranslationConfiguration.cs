using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Translation.Models.Configuration
{
    public class FunTranslationConfiguration : IEntityTypeConfiguration<FunTranslation>
    {
        public void Configure(EntityTypeBuilder<FunTranslation> builder)
        {
            builder.HasData(
                new FunTranslation
                {
                    Id = 1,
                    NormalText = "sefa",
                    TranslatedText = "s3f4"
                },
                new FunTranslation
                {
                    Id = 2,
                    NormalText = "sefaca",
                    TranslatedText = "s3f4c4"
                }
            );
        }
    }
}
