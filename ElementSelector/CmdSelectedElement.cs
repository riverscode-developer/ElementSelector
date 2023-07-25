using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;

namespace ElementSelector
{
    [Transaction(TransactionMode.Manual)]
    public class CmdSelectedElement : IExternalCommand  // Interfaz == contrato
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Variables de proyecto (Ensenciales) 
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Selection selection = uiDoc.Selection;
            ICollection<ElementId> selectedElementId = selection.GetElementIds();

            Rebar element = doc.GetElement(selectedElementId.First()) as Rebar;
            ElementId idHost =  element.GetHostId();
            Element elementHost = doc.GetElement(idHost);

            
            // Filtrar todos los elementos de tipo rebar / donde su hostId == al id del elemento seleccionado

            //Utils utils = new Utils(); // Instanciando un clase (Creando un objeto)
            //Utils.ShowMessage(selectedElementId.Count.ToString(), MessageType.Error);
            Utils.ShowMessage(elementHost.Name, MessageType.Information);


            return Result.Succeeded;
        }
    }
}
