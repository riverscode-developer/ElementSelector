using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace ElementSelector
{
    [Transaction(TransactionMode.Manual)]
    public class CmdFilteredElementCollector: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            #region FilteredElementCollector - OfCategory
            //FilteredElementCollector collectorA = new FilteredElementCollector(doc);
            //collectorA.OfCategory(BuiltInCategory.OST_StructuralFraming); // Filtro por categoria => BuiltInCategory.OST_StructuralColumns
            //collectorA.WhereElementIsNotElementType(); // Filtro por tipo de elemento => Elementos que no son tipos
            //List<Element> columns = collectorA.ToElements().ToList();

            //Utils.ShowElements(columns); 
            #endregion


            #region FilteredElementCollector - OfClass
            // FilteredElementCollector collectorB = new FilteredElementCollector(doc);

            //collectorB.OfClass(typeof(WallType));
            ////collectorB.OfCategory(BuiltInCategory.OST_StructuralFraming);
            // List<Element> elementsInView = collectorB.ToElements().ToList();
            //Utils.ShowElements(elementsInView);
            #endregion


            List<BuiltInCategory> categories = new List<BuiltInCategory> { BuiltInCategory.OST_StructuralFraming, BuiltInCategory.OST_StructuralColumns};
            ElementMulticategoryFilter elementMulticategoryFilter = new ElementMulticategoryFilter(categories);

            FilteredElementCollector collectorC = new FilteredElementCollector(doc);
            collectorC.WherePasses(elementMulticategoryFilter);
            collectorC.WhereElementIsNotElementType();

            List<Element> elementList = collectorC.ToElements().ToList();

            Utils.ShowElements(elementList);

            return Result.Succeeded;
        }
    }
}

