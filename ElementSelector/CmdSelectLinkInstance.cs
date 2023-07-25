using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;

namespace ElementSelector
{
    [Transaction(TransactionMode.Manual)]
    public class CmdSelectLinkInstance : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;
            try
            {

                IList<Reference> references = uiDoc.Selection.PickObjects(ObjectType.LinkedElement);
                RevitLinkInstance selectedRevitLinkInstance = doc.GetElement(references[0]) as RevitLinkInstance;
                Document linkedDocument = selectedRevitLinkInstance.GetLinkDocument();

                Element selectedElement = linkedDocument.GetElement(references[0].LinkedElementId);
                //// Lista de elemtos [e1, e2, e3, e4, e5, e6, e7, e8, e9, e10]

                Utils.ShowMessage(message: selectedElement.Category.Name, messageType: MessageType.Information);
            }
            catch (System.Exception ex)
            {
                Utils.ShowMessage(message: ex.Message, messageType: MessageType.Error);
                return Result.Cancelled;
            }
            


            return Result.Succeeded;
        }
    }


}
