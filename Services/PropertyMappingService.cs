using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> PropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
            {"ApplicantId", new PropertyMappingValue (new List<string>(){"ApplicantId"}) },
            {"Name", new PropertyMappingValue (new List<string>(){"FirstName", "SecondName", "LastName"}) },
           
           /* {"PlaceOfBirth", new PropertyMappingValue (new List<string>(){"PlaceOfBirth"}) },
            {"DateOfBirth", new PropertyMappingValue (new List<string>(){"DateOfBirth"}, true) },
            {"Nationality", new PropertyMappingValue (new List<string>(){"Nationality"}) },
            {"Relegion", new PropertyMappingValue (new List<string>(){"Relegion"}) },
            {"Mobile", new PropertyMappingValue (new List<string>(){"Mobile"}) },
            {"Gender", new PropertyMappingValue (new List<string>(){"Gender"}) },
            {"FamilyStatus", new PropertyMappingValue (new List<string>(){"FamilyStatus"}) },
            {"SpokenLanguage", new PropertyMappingValue (new List<string>(){"SpokenLanguage"}) },
            {"Status", new PropertyMappingValue (new List<string>(){"Status"}) },*/
        };

        private IList<IPropertyMapping> PropertyMappings = new List<IPropertyMapping>();
        public PropertyMappingService()
        {
            PropertyMappings.Add(new PropertyMapping<ApplicantDto, Applicant>(PropertyMapping));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = PropertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");

        }



    }
    }

