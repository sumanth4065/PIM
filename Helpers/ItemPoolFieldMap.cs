using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMQAAutomation.Helpers
{
    public static class ItemPoolFieldMap
    {
        public static readonly Dictionary<string, Func<ItemRecord, string>> DBItemPoolFieldMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["ItemId"] = item => item.Sku,
                ["OEM"] = item => item.Oem.Name,
                ["Geography"] = item => item.Geography.Type,
                ["ProductClass"] = item => item.Taxonomy.Class.Name,
                ["ProductCategory"] = item => item.Taxonomy.Category.Name,
                ["Description"] = item => item.Description,
                ["ProductFamily"] = item => item.Taxonomy.Family.Name,
                ["ProductLine"] = item => item.Taxonomy.Line.Name,
                ["ProductGroup"] = item => item.Taxonomy.Group.Name,
                ["IsComponent"] = item => item.General.IsComponent.ToString(),
                ["IsProduct"] = item => item.General.IsProduct.ToString(),
                ["IsActive"] = item => item.General.IsActive.ToString(),
                ["IsValid"] = item => item.General.IsValid.ToString(),
                ["IsArchived"] = item => item.General.IsArchived.ToString(),
                ["IsRemoved"] = item => item.General.IsRemoved.ToString(),
                ["IsTest"] = item => item.General.IsTest.ToString(),
                ["IsAlias"] = item => item.General.IsAlias.ToString(),
                ["IsManaged"] = item => item.General.IsManaged.ToString(),
                ["IsPublishable"] = item => item.General.IsPublishable.ToString(),
                ["IsCalculated"] = item => item.General.IsCalculated.ToString(),
                ["ExternalNotes"] = item => item.ExternalNotes ?? "",
                ["InternalNotes"] = item => item.InternalNotes ?? "",
                ["LOBName"] = item => item.ItemLineOfBusinesses?.FirstOrDefault(lob => lob.ServiceProcessingType == "Primary")?.Name ?? "",
                ["LOBDescription"] = item => item.ItemLineOfBusinesses?.FirstOrDefault(lob => lob.ServiceProcessingType == "Primary")?.Description ?? "",
                ["LOBIsActive"] = item => item.ItemLineOfBusinesses?.FirstOrDefault(lob => lob.ServiceProcessingType == "Primary")?.General.IsActive.ToString() ?? "",
                ["LOBIsValid"] = item => item.ItemLineOfBusinesses?.FirstOrDefault(lob => lob.ServiceProcessingType == "Primary")?.General.IsValid.ToString() ?? "",
                ["LOBIsRemoved"] = item => item.ItemLineOfBusinesses?.FirstOrDefault(lob => lob.ServiceProcessingType == "Primary")?.General.IsRemoved.ToString() ?? "",
                ["LOBInternalNotes"] = item => item.ItemLineOfBusinesses?.FirstOrDefault(lob => lob.ServiceProcessingType == "Primary")?.InternalNotes ?? ""
            };
    }
}
