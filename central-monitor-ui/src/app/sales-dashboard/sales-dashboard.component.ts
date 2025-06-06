import { Component, OnInit } from '@angular/core';
import { SalesService } from '../services/sales.service';
import { NgForOf, NgIf } from '@angular/common';

type SalesData = {
  unitNo: string,
  totalAmount: Number
};

@Component({
  selector: 'app-sales-dashboard',
  standalone: true,
  imports: [NgIf, NgForOf],
  templateUrl: './sales-dashboard.component.html',
  styleUrls: ['./sales-dashboard.component.scss']
})

export class SalesDashboardComponent implements OnInit {
  salesData: SalesData[] = [];
  isLoading = true;

  constructor(private salesService: SalesService) {}

  ngOnInit(): void {
    this.salesService.getTotalSales().subscribe({
      next: (data) => {
        Object.entries(data).forEach(([index, item]) => {       
            const { unitNo, totalAmount } = item as any as SalesData;
            this.salesData.push({
              unitNo: unitNo as string,
              totalAmount: Number(totalAmount) as Number
            } as SalesData);
        });
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading sales:', err);
        this.isLoading = false;
      }
    });
  }
}