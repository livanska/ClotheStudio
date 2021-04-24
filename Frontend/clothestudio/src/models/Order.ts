import { OrderedItem } from "./OrderedItem";

export interface Order{
    employeeID:number;
    customerID?:number;
    statusID?:number;
    expectedDeadlineTime:Date;
    orderedItems: OrderedItem[];
}

export const defaultOrder:Order={
    employeeID:0,
    expectedDeadlineTime:new Date,
    orderedItems: [],
}