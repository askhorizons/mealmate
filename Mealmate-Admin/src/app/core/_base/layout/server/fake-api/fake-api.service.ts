// Angular
import { Injectable } from "@angular/core";
// Angular in memory
import { InMemoryDbService } from "angular-in-memory-web-api";
// RxJS
import { Observable } from "rxjs";
// Auth
import { AuthDataContext } from "../../../../auth";
// ECommerce
import { MenuDataContext } from "../../../../menu2";
import { ECommerceDataContext } from "../../../../e-commerce";
// Models
import { CarsDb } from "./fake-db/cars";

@Injectable()
export class FakeApiService implements InMemoryDbService {
  /**
   * Service Constructors
   */
  constructor() {}

  /**
   * Create Fake DB and API
   */
  createDb(): {} | Observable<{}> {
    // tslint:disable-next-line:class-name
    const db = {
      // auth module
      users: AuthDataContext.users,
      roles: AuthDataContext.roles,
      permissions: AuthDataContext.permissions,

      // e-commerce
      // customers
      customers: ECommerceDataContext.customers,
      // products
      products: ECommerceDataContext.cars,
      productRemarks: ECommerceDataContext.remarks,
      productSpecs: ECommerceDataContext.carSpecs,

      // orders
      orders: ECommerceDataContext.orders,

      // Menu
      // customers
      menu: MenuDataContext.customers,
      // products
      products1: MenuDataContext.cars,
      productRemarks1: MenuDataContext.remarks,
      productSpecs1: MenuDataContext.carSpecs,

      // orders
      orders1: MenuDataContext.orders,

      // data-table
      cars: CarsDb.cars,
    };
    return db;
  }
}
