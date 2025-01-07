class LocalStorage {
    static GetToken<T>(): T | null {
        const value = localStorage.getItem("Token");
        if (value) {
            try {
                return value as T;
            } catch (error) {
                console.log("Error convert json in LocalStorage Key Token");
                return null;
            }
        }
        return null;
    }
    static GetUserName<T>(): T | null {
        const value = localStorage.getItem("UserName");
        if (value) {
            try {
                return value as T;
            } catch (error) {
                console.log("Error convert json in LocalStorage Key Token");
                return null;
            }
        }
        return null;
    }

}
export class LocalStorageService{
    static GetToken<T>(): T | null{
        return LocalStorage.GetToken();
    }
    static GetUserName(): string | null{
        return LocalStorage.GetUserName();
    }
}