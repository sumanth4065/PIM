namespace PIMQAAutomation.Helpers.DocumentFetcher
{
    /// <summary>
    /// SourceRules provides a mapping between document type names and their corresponding sources.
    /// This avoids strong coupling to external library types and is resilient to namespace/class changes.
    /// </summary>
    public static class SourceRules
    {
        /// <summary>
        /// Dictionary mapping type names (as strings) to their corresponding DocumentSource.
        /// </summary>
        private static readonly Dictionary<string, DocumentSource> TypeNameToSource = new()
        {
            { "Item", DocumentSource.EventAudit },
            { "ItemCatalog", DocumentSource.EventAudit },
            { "Alternate", DocumentSource.IDH },
            { "Bom", DocumentSource.IDH },
            { "LineOfBusiness", DocumentSource.EventAudit }
        };

        /// <summary>
        /// Gets the document source for a type of name T.
        /// </summary>
        /// <typeparam name="T">The document type.</typeparam>
        /// <returns>The corresponding DocumentSource.</returns>
        /// <exception cref="NotSupportedException">Thrown if the type name is not mapped.</exception>
        public static DocumentSource GetSourceForType<T>()
        {
            return GetSourceForTypeName(typeof(T).Name);
        }

        /// <summary>
        /// Gets the document source for a given type name string.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>The corresponding DocumentSource.</returns>
        /// <exception cref="ArgumentNullException">Thrown if typeName is null or empty.</exception>
        /// <exception cref="NotSupportedException">Thrown if typeName is not mapped.</exception>
        public static DocumentSource GetSourceForTypeName(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName), "Type name cannot be null or empty.");

            if (!TypeNameToSource.TryGetValue(typeName, out var source))
                throw new NotSupportedException($"No source mapping defined for type {typeName}");

            return source;
        }
    }
}