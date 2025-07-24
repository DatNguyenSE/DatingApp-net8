import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl ='https://localhost:5001/api/';
  currentUser = signal<User | null>(null);
  
  login(model: any) {
    return this.http.post<User>(this.baseUrl+'account/login',model).pipe(        //.pipe(...): Cho phép bạn xử lý dữ liệu trả về trước khi gửi ra ngoài.
      map(user => {                                                           //map() dùng để biến đổi dữ liệu.
        if(user) {
          localStorage.setItem("user", JSON.stringify(user));               // đổi về dạng object -> txtjson sau đó muốn lấy thì JSON.parse(localStorage.getItem("user")) 
          this.currentUser.set(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl+'account/register',model).pipe( //.pipe(...): Cho phép bạn xử lý dữ liệu trả về trước khi gửi ra ngoài.
      map(user => {                                                     //map() dùng để biến đổi dữ liệu.
        if(user) {
          localStorage.setItem("user", JSON.stringify(user)); // đổi về dạng object -> txtjson sau đó muốn lấy thì JSON.parse(localStorage.getItem("user")) 
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }

  logout(){
    localStorage.removeItem('user')
    this.currentUser.set(null);
  }
}
/*Nếu server trả về object user:

Lưu thông tin người dùng vào localStorage, để giữ trạng thái đăng nhập sau khi reload trình duyệt.

Cập nhật signal currentUser → để UI hoặc các thành phần khác có thể biết người dùng đang đăng nhập.
*/

