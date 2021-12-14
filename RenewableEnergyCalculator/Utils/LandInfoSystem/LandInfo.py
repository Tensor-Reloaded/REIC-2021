import geopandas
import pandas as pd
from shapely.geometry import Point

class LandData(object):
    __instance = None

    def __new__(cls, *args, **kwargs):
        if not LandData.__instance:
            LandData.__instance = object.__new__(cls)
        return LandData.__instance

    def __init__(self):
        path_to_data = 'romania.gpkg'
        self.gdf = geopandas.read_file(path_to_data, layer='romania')

        path_to_legend = 'CLC_legend.csv'
        self.colordf = pd.read_csv(path_to_legend, error_bad_lines=False, sep=";", index_col=0)

        path_to_reject = 'reject.csv'
        rejectdf = pd.read_csv(path_to_reject, error_bad_lines=False, sep=";")
        self.rejectlist = list(rejectdf['CLC_CODE'])

    def convert(self, pointX, pointY):
        selected_point = {'col1': ['name1'], 'geometry': [Point(pointX, pointY)]}
        gdf = geopandas.GeoDataFrame(selected_point, crs=4326)
        gdf = gdf.to_crs(3035)
        return gdf['geometry'][0]

    def get_type(self, latitude, longitude):
        response = {}
        EPSG3035_coords = self.convert(longitude, latitude)
        selected_point = EPSG3035_coords
        json_found = False
        json_rejected = False
        json_label = ''
        json_remark = ''

        for polygon, code, remark in zip(self.gdf['geometry'], self.gdf['Code_18'], self.gdf['Remark']):
            if selected_point.within(polygon):
                json_label = list(self.colordf[self.colordf['CLC_CODE'] == int(code)]['LABEL3'])[0]
                json_found = True
                json_remark = remark

            if int(code) in self.rejectlist:
                json_rejected = True
        
        response['found'] = json_found
        response['rejected'] = json_rejected
        response['label'] = json_label
        response['remark'] = json_remark

        print(response)


land = LandData()
land.get_type(47.17489572844038, 27.55378958999128)