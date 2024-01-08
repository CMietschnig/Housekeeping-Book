import type { IInvoice } from "@/interfaces/IInvoice";
import type { AxiosResponse } from "axios";
import axios from "axios";

class InvoicesApiService {
    async updateInvoiceById(id: number, invoice: IInvoice): Promise<AxiosResponse<IInvoice>> {
        return axios.post('');
    }
}