Install-Package FontAwesome.WPF -Version 4.7.0.9

App.xamp
  <Application.Resources>
        <fa:CssClassNameConverter Mode="FromIconToString" x:Key="FromIconClassNameConverter" />
        <fa:CssClassNameConverter Mode="FromStringToIcon" x:Key="FromStringClassNameConverter" />
   </Application.Resources>
