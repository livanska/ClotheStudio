export interface OrderedItem {
    serviceID?:number;
    description?:string;
    reqMaterials:{
        materialID:number;
        materialName?:string;
        materialAmount:number;
        materialCost:number;
    }[]
    orderID?:number;
    employeeID?:number;
    serviceName?:string;
    serviceCost:number;
}
export const defaultOrderedItem:OrderedItem = {
    serviceID:1,
    reqMaterials:[],
    description:'',
    serviceName:'',
    serviceCost:0,

}
