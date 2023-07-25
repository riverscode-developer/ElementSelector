
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace ElementSelector
{
    public class Utils
    {
        /// <summary>
        /// Muestra un mensaje en pantalla personalizado
        /// </summary>
        /// <param name="message">Mensaje al usuario</param>
        /// <param name="messageType">Tipo de mensaje</param>
        public static void ShowMessage (string message, MessageType messageType = MessageType.Information)
        {
            TaskDialog myWindow = new TaskDialog("Mi Ventana");
            myWindow.MainContent = message;
            myWindow.FooterText = "Lambda Ingenieria e Innovación 2023";
            myWindow.TitleAutoPrefix = false;
            myWindow.ExpandedContent = "Bienvenido a lambda ingenieria e innovación para aprender C# y el uso de la API de Revit";

            if(messageType == MessageType.Error)
            {
                myWindow.MainInstruction = "Mensaje Error";
                myWindow.MainIcon = TaskDialogIcon.TaskDialogIconError; // enum ( Lista de constantes ya definidas)
            }
            else if(messageType == MessageType.Information)
            {
                myWindow.MainInstruction = "Mensaje Informativo";
                myWindow.MainIcon = TaskDialogIcon.TaskDialogIconInformation; // enum ( Lista de constantes ya definidas)
            }
            else if (messageType == MessageType.Warning)
            {
                myWindow.MainInstruction = "Mensaje Advertencia";
                myWindow.MainIcon = TaskDialogIcon.TaskDialogIconWarning; // enum ( Lista de constantes ya definidas)
            }
            else
            {
                myWindow.MainIcon = TaskDialogIcon.TaskDialogIconNone; // enum ( Lista de constantes ya definidas)
            }


            myWindow.Show();
        }

        /// <summary>
        /// Permite seleccionar elementos en la vista activa
        /// </summary>
        /// <param name="uiDoc">Variable UiDocument</param>
        /// <param name="promp">Mensaje que se le muestra al usuario para seleccionar elementos</param>
        /// <returns>Lista de Referencias de los elementos seleccionados</returns>
        public static (IList<Reference>, bool isDone ) SelectElements(UIDocument uiDoc, string promp = "Seleccione elementos") {
            try
            {
                IList<Reference> references = uiDoc.Selection.PickObjects(ObjectType.Element, promp);

                if (references.Count == 0)
                {
                    Utils.ShowMessage("No se selecciono ningun elemento", MessageType.Warning);
                    return (null, false);
                }

                uiDoc.RefreshActiveView();
                return (references, true);
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                Utils.ShowMessage("El usuario aborto la operación de selección", MessageType.Error);
                // Enviar un mensaje a un correo
                // Enviar a una de datos 
                // Enviar a log de errores
                return (null, false);
            }
        }

        /// <summary>
        /// Permite seleccionar elementos aplicandole un filtro de seleccion en la vista activa
        /// </summary>
        /// <param name="uiDoc">Variable UiDocument</param>
        /// <param name="selectionFilter">Filtro de seleccion que implementa IFilterSelección</param>
        /// <param name="promp">Mensaje que se le muestra al usuario para seleccionar elementos</param>
        /// <returns>Lista de Referencias de los elementos seleccionados</returns>
        public static IList<Reference> SelectElements(UIDocument uiDoc, ISelectionFilter selectionFilter, string promp = "Seleccione elementos")
        {

            IList<Reference> references = uiDoc.Selection.PickObjects(ObjectType.Element, selectionFilter, promp);

            if (references.Count == 0)
            {
                Utils.ShowMessage("No se selecciono ningun elemento", MessageType.Warning);
            }

            uiDoc.RefreshActiveView();

            return references;
        }
    
        /// <summary>
        /// Permite seleccionar un elemento en la vista activa
        /// </summary>
        /// <param name="elements"></param>
        public static void ShowElements(List<Element> elements)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Elementos Seleccionados: {elements.Count}\n\n");
            foreach (Element element in elements)
            {
                sb.AppendLine($"{element.Id} - {element.GetType().Name} - {element.Category.Name} - {element.Name}\n ");
            }
            Utils.ShowMessage(sb.ToString(), MessageType.Information);
        }   
    
        public static string GetParameterValueAsString(Parameter parameter)
        {
            StorageType storageType = parameter.StorageType;

            if(storageType == StorageType.String) return parameter.AsValueString();
            if(storageType == StorageType.Integer) return parameter.AsInteger().ToString();
            if(storageType == StorageType.Double) return parameter.AsDouble().ToString();
            if(storageType == StorageType.ElementId) return parameter.AsElementId().ToString();

            return string.Empty;
        }
    }


    public enum MessageType
    {
        Information = 0,
        Error = 1,
        Warning = 2 
    }
}
