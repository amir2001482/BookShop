using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Class
{
    public static class EnumExtentions
    {
        public static List<string> ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));
            List<string> Messages = new List<string>();

            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttributes(false).FirstOrDefault();

            if (attribute == null)
                return Messages;

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            Messages.Add(propValue.ToString());
            return Messages;
        }
    }
    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
