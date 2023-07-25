using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace ElementSelector
{
    [Transaction(TransactionMode.Manual)]
    public class CmdParameters : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            // Get all system parameters name in project
            //FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfClass(typeof(ParameterElement)); // Compartidos, proyecto, familia, globales

            #region ❌ NO SE PUEDE TRABAJAR CON PARAMETROS
            //collector.OfClass(typeof(Parameter)); // Todos los parametros

            //ICollection<ElementId> parameter = collector.ToElementIds();

            //StringBuilder sb = new StringBuilder();

            //foreach (ElementId elementId in parameter)
            //{
            //    Element parameterA = doc.GetElement(elementId);
            //    sb.AppendLine(parameterA.Name);
            //}
            #endregion



            Reference reference = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

            Element element = doc.GetElement(reference);



            // Utils.ShowMessage($"Usted selecciono un elemento de la categoria {element.Category.Name}", MessageType.Information);

            #region LOOKUP PARAMETER
            //Parameter parameterA = element.LookupParameter("Level");

            //Utils.ShowMessage(parameterA.StorageType.ToString());

            //string parameterValueAsString = parameterA.AsValueString();
            //double parameterAsDouble = parameterA.AsDouble();
            //ElementId parameterAsElementId = parameterA.AsElementId();

            //Element level = doc.GetElement(parameterAsElementId);

            //Parameter levelParameter = level.LookupParameter("Elevation");
            //string levelParameterValueAsString = levelParameter.AsValueString();
            #endregion

            #region GET PARAMETER
            // GUID Es unico para cada parametro
            //Parameter parameterA = element.get_Parameter(BuiltInParameter.SCHEDULE_LEVEL_PARAM);

            //Utils.ShowMessage(parameterA.StorageType.ToString());

            //string parameterValueAsString = parameterA.AsValueString();
            //double parameterAsDouble = parameterA.AsDouble();
            //ElementId parameterAsElementId = parameterA.AsElementId();

            //Element level = doc.GetElement(parameterAsElementId);

            //Parameter levelParameter = level.get_Parameter(BuiltInParameter.LEVEL_ELEV);
            //string levelParameterValueAsString = levelParameter.AsValueString();
            #endregion

            #region PARAMETERMAP
            //ParameterMap parameterMap = element.ParametersMap;
            //Parameter parameterA = parameterMap.get_Item("Nivel del Elemento");
            #endregion



            #region PARAMETER AS STRING
            //Parameter parameterA = element.get_Parameter(BuiltInParameter.SCHEDULE_LEVEL_PARAM);
            //string stringValue = Utils.GetParameterValueAsString(parameterA);
            #endregion

            #region PARAMETER GetParameters
            //List<Parameter> parameterA = element.GetParameters("Nivel del Elemento").ToList();
            #endregion

            #region PARAMETER GetParameters
            //IList<Parameter> parameters = element.GetOrderedParameters();

            //List<string> parameterNames = (from p in parameters
            //                               select p.Definition.Name).ToList();
            #endregion



            #region PARAMETROS USER-DEFINED
            //BindingMap bindingMap = doc.ParameterBindings;

            //DefinitionBindingMapIterator iterator = bindingMap.ForwardIterator(); // IEnumerable [1,2,3,4,5,6,7,8,9,10]


            //List<string> allParameters = new List<string>();
            //// Obtener todos los parametros de tipo texto
            //while (iterator.MoveNext())
            //{
            //    Definition definition = iterator.Key;
            //    allParameters.Add(definition.Name);
            //    //Utils.ShowMessage(definition.Name, MessageType.Information);
            //}
            #endregion

            #region ParameterSet
            //ParameterSet parameterSet = element.Parameters;
            //ParameterSetIterator iterator = parameterSet.ForwardIterator();

            //while (iterator.MoveNext())
            //{
            //    Parameter parameter = iterator.Current as Parameter;
            //}
            #endregion

            // #########


            #region LOOKUP PARAMETER

            ElementId typeId = element.GetTypeId();
            Element type = doc.GetElement(typeId);

            Parameter parameterA = type.LookupParameter("b1");
            string parameterValueAsString = Utils.GetParameterValueAsString(parameterA);

            #endregion


            return Result.Succeeded;
        }
    }
}