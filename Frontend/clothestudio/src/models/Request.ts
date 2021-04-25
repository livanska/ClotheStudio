export interface Request {
    atelieID?:number;
    employeeID:number;
    companyID?:number;
    expectedDeadlineTime:string;
    address:string;
    country:string;
    name:string;
    city:string;
    requestedMaterials:{
        materialID:number;
        materialName?:string;
        materialAmount:number;
        materialCost:number;
    }[]
}

export const defaultRequest:Request = {
    employeeID:0,
    expectedDeadlineTime: '',
    requestedMaterials:[],
    address:'',
    country:'',
    name:'',
    city:'',

}