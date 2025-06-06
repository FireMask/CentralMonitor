import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SalesService {
  constructor(private http: HttpClient) {}

  getTotalSales(): Observable<{ [storeId: string]: number }> {
    return this.http.get<{ [storeId: string]: number }>(`${environment.apiUrl}/sales/total-sales-ef`);
  }
}
