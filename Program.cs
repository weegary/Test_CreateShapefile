using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

Console.WriteLine("This program is written by Gary Wee.");
CreatePolygonShpFile();
Console.WriteLine(@"A shapefile named 'Shapefile' is created.");

void CreatePolygonShpFile()
{
    FeatureCollection features = new FeatureCollection();
    Coordinate[] coordinates = new Coordinate[5];
    double x = 120;
    double y = 20;
    double interval = 10;
    coordinates[0] = new Coordinate(x, y);
    coordinates[1] = new Coordinate(x + interval, y);
    coordinates[2] = new Coordinate(x + interval, y - interval);
    coordinates[3] = new Coordinate(x, y - interval);
    coordinates[4] = new Coordinate(x, y);
    
    LinearRing lr = new LinearRing(coordinates);
    Polygon polygon = new Polygon(lr);

    Feature feature = new Feature();
    feature.Geometry = polygon;

    AttributesTable attributeTable = new AttributesTable();
    attributeTable.Add("ID", 1);
    attributeTable.Add("x", x);
    attributeTable.Add("y", y);
    feature.Attributes = attributeTable;
    features.Add(feature);

    string file_name = "Shapefile.shp";
    
    ShapefileDataWriter sdw = new ShapefileDataWriter(file_name, GeometryFactory.Default);
    sdw.Header = ShapefileDataWriter.GetHeader(features[0], features.Count);
    sdw.Write(features);

    string proj_file_name = file_name.Replace(".shp", ".prj");
    using (var streamWriter = new StreamWriter(proj_file_name))
    {
        streamWriter.Write(ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84.WKT);
    }
}