namespace MediaLibrary
{
    public enum FieldDataTypes: int
    {
        /// <summary> Текстовое значение 
        /// </summary>
        Text = 0,
        /// <summary> Изображение 
        /// </summary>
        Image = 1,
        /// <summary> Путь к ресурсу
        /// </summary>
        Path = 2,
        /// <summary> Дата/время 
        /// </summary>
        DateTime = 3,
        /// <summary> ссылка на ресурс (uri)
        /// </summary>
        Hyperlink = 4,
        // remember, what is this? Возможно, это поле-уведомление о каких-то специальных свойствах,
        //  напр. плохое качество материала. Это поле может влиять на отображение ресурса в родительском списке - 
        //  помечать его цветом или значком.
        Notification = 5, 
        /// <summary> ссылка на другой элемент 
        /// </summary>
        LinkToItem = 6,
        /// <summary> элемент справочника
        /// </summary>
        ItemOf = 7,
        /// <summary>
        /// Набор значений справочника
        /// </summary>
        SetOfItems = 8,
    }

    /// <summary> Роли полей, их значимость 
    /// </summary>
    public enum FieldRoles : int
    {
        /// <summary> Обычное поле 
        /// </summary>
        Default = 0,
        /// <summary> Поле с названием ресурса 
        /// </summary>
        Name = 1,
        /// <summary> Поле с описанием ресурса 
        /// </summary>
        Description = 2,
        /// <summary> Поле, содержащее логотип ресурса 
        /// </summary>
        Logo = 3,
        /// <summary> Путь 
        /// </summary>
        Path = 4,
    }
}
