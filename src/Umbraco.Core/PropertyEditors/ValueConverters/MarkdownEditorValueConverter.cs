﻿using System;
using System.Web;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Core.PropertyEditors.ValueConverters
{
    public class MarkdownEditorValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return Constants.PropertyEditors.MarkdownEditorAlias.Equals(propertyType.PropertyEditorAlias);
        }

        public override Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return typeof (IHtmlString);
        }

        public override PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType)
        {
            // PropertyCacheLevel.Content is ok here because that converter does not parse {locallink} nor executes macros
            return PropertyCacheLevel.Content;
        }

        public override object ConvertSourceToInter(PublishedPropertyType propertyType, object source, bool preview)
        {
            // in xml a string is: string
            // in the database a string is: string
            // default value is: null
            return source;
        }

        public override object ConvertInterToObject(PublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            // source should come from ConvertSource and be a string (or null) already
            return new HtmlString(inter == null ? string.Empty : (string) inter);
        }

        public override object ConvertInterToXPath(PublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            // source should come from ConvertSource and be a string (or null) already
            return inter?.ToString() ?? string.Empty;
        }
    }
}