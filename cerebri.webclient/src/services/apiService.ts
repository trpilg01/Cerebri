import axios, { AxiosInstance } from "axios";
import { LoginRequest, Mood } from "data/dataTypes";

const apiClient: AxiosInstance = axios.create({
    baseURL: "http://localhost:5091/api",
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',
    },
});

apiClient.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

apiClient.interceptors.response.use(
    (response) => response,
    (error) => {
        console.error('API Error:', error.response?.data || error.message);
        return Promise.reject(error);
    }
);

export const requestLogin = async (request: LoginRequest) => {
    try {
        const response = await apiClient.post('/Auth/login', request);
        localStorage.setItem("token", response.data["token"]);
        return true;
    } catch (error: any) {
        console.log(error.message);
        return error.message;
    }
};

export const requestMoods = async () => {
    try {
        const response = await apiClient.get('/Mood');
        console.log(response);

        let moods: Mood[] = [];
        response.data.forEach((item : any) => {
            const newMood: Mood = {
                id: item["id"],
                name: item["name"],
                moodType: item["type"]
            };
            moods.push(newMood);
        });
        //console.log("Moods: ", moods);
        return moods;
    } catch (error: any) {
        console.log(error.message);
        return error.message;
    }
}