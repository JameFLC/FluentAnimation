using FluentAnimatinon.PropertyEditor.Amalyzer;
using FluentAnimatinon.PropertyEditor.Editable;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Reflection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation.Controls.PropertyEditor
{
    public sealed partial class ProceduralControl : UserControl
    {
        #region Properties
        
        public static readonly DependencyProperty EditableObjectProperty =
            DependencyProperty.Register("EditableObject", typeof(IEditable), typeof(ProceduralControl), new PropertyMetadata(0));

        #endregion

        public ProceduralControl()
        {
            this.InitializeComponent();
        }

        public IEditable EditableObject
        {
            get { return (IEditable)GetValue(EditableObjectProperty); }
            set { SetValue(EditableObjectProperty, value); GenerateUI(); }
        }

        private void GenerateUI()
        {
            if (EditableObject is not null)
            {
                List<PropertyInfo> properties = EditablePropertyFinder.FindEditableProperties(EditableObject);

                foreach (PropertyInfo property in properties)
                {
                    object? value = property.GetValue(EditableObject);

                    Control newControl = value switch
                    {
                        bool x => new BaseControl.BoolControl(),
                        int x => new BaseControl.IntControl(),
                        string x => new BaseControl.StringControl(),
                        _ => new BaseControl.UnhandledControl(),
                    };
                    Pane.Children.Add(newControl);
                }
            }
            else
            {
                Pane.Children.Clear();   
            }
        }


    }
}
