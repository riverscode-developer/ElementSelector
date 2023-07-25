using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ElementSelector
{
    [Transaction(TransactionMode.Manual)]
    public class CmdExportExcel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Reference reference = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

            Element element = doc.GetElement(reference);

            Utils.ShowMessage("La proxima clase veremos interoperabilidad con excel");

            return Result.Succeeded;
        }
    }
}
