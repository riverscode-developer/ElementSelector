using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace ElementSelector
{
    [Transaction(TransactionMode.Manual)]
    public class CmdSelectElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Enviar un mensaje a una base de datos (Log) => Hora, Usuario, Proyecto, Version, addin

            // Variables de proyecto (Ensenciales) 
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            //ISelectionFilter columnFilter = new StructuralColumnFilter();
            //IList<Reference> references = Utils.SelectElements(uiDoc, columnFilter); // Encapsulamiento
            var (references, isDone) = Utils.SelectElements(uiDoc); // Encapsulamiento
            if (!isDone) return Result.Cancelled;

            List<Element> elementList = references.Select(refe => doc.GetElement(refe)).ToList();

            // Calculo del volumen de los elementos seleccionados
            Utils.ShowMessage(references.Count.ToString(), MessageType.Information);

            return Result.Succeeded;
        }
    }

    public class StructuralColumnFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {

            // operadores logicos =>  && (and) || (or) ! (not)  
            // V && V = V
            // V && F = F
            // F && V = F
            // F && F = F

            // V || V = V 
            // V || F = V
            // F || V = V
            // F || F = F

            if ((BuiltInCategory)elem.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralColumns ||
                (BuiltInCategory)elem.Category.Id.IntegerValue == BuiltInCategory.OST_Walls ||
                (BuiltInCategory)elem.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFraming)
            {
                return true;
            }

            //if ((BuiltInCategory)elem.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralColumns && elem.Name.Contains("Columna"))
            //{
            //    //if(elem.Name.Contains("Columna"))
            //    //{
            //        return true;
            //    //}
            //}



            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }

}
