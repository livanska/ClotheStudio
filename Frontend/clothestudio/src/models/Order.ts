import { OrderedItem } from "./OrderedItem";

export interface Order{
    employeeID:number;
    customerID?:number;
    statusID?:number;
    atelieID?:number
    expectedDeadlineTime:string;
    orderedItems: OrderedItem[];
    firstname?:string
    lastname?:string
    phoneNumber?:string
}

export const defaultOrder:Order={
    employeeID:0,
    expectedDeadlineTime: '',
    orderedItems: [],
    customerID:-1
}