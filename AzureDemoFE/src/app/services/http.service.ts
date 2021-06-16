import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FileRequest } from 'src/app/models/file-request.model';
import { Observable } from 'rxjs';
import { DecodeRequest } from 'src/app/models/decode-request.model';
import { DecodeResponse } from 'src/app/models/decode-response.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  baseUrl = 'https://demo-function-http.azurewebsites.net/api';

  constructor(private httpClient: HttpClient) {
  }

  uploadContent(fileRequest: FileRequest): Observable<null> {
    return this.httpClient.post<null>(`${this.baseUrl}/text`, fileRequest);
  }

  decodeContent(decodeRequest: DecodeRequest): Observable<string> {
    return this.httpClient.post<DecodeResponse>(`${this.baseUrl}/decode`, decodeRequest)
      .pipe(map(response => response.decodedText));
  }
}
