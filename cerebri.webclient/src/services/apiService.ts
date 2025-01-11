import axios, { AxiosInstance } from "axios";
import { CreateJournalRequest, LoginRequest, Mood, Journal, CheckIn, CreateCheckInDTO, ReportData, UpdatedReport, DeleteReport, CreateReportRequest, UserInfo } from "data/dataTypes";

const apiClient: AxiosInstance = axios.create({
    baseURL: "http://localhost:5091/api",
    timeout: 50000,
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
    const response = await apiClient.post('/Auth/login', request);
    localStorage.setItem("token", response.data["token"]);
    return response;
};

export const requestMoods = async () => {
    try {
        const response = await apiClient.get('/Mood');

        let moods: Mood[] = [];
        response.data.forEach((item : any) => {
            const newMood: Mood = {
                id: item["id"],
                name: item["name"],
                type: item["type"]
            };
            moods.push(newMood);
        });
        return moods;
    } catch (error: any) {
        console.log(error.message);
        return error.message;
    }
}

export const requestJournals = async () => {
    try {
        const response = await apiClient.get("/JournalEntry");
        
        const journals: Journal[] = []
        response.data.forEach((item : any) => {
            const entry: Journal = {
                id: item["id"],
                title: item["title"],
                content: item["content"],
                moods: item["moods"],
                createdAt: item["createdAt"]
            }
            journals.push(entry);
        });

        return journals;
    } catch (error : any){
        console.log(error.message);
    }
}

export const requestCreateJournal = async (request: CreateJournalRequest) => {
    try {
        await apiClient.post("/JournalEntry/create", request);
    } catch (error: any) {
        console.log(error, error.message);
    }
}

export const requestDeleteJournal = async (entryId: string) => {
    try {
        const response = await apiClient.post('/JournalEntry/delete', {
            "entryId": entryId
        });
        return response.data;
    } catch (error: any) {
        console.log(error, error.message);
    }
}

export const requestCheckIns = async () => {
    try {
        const response = await apiClient.get('/CheckIn');
        const checkIns: CheckIn[] = []

        response.data.forEach((item : any) => {
            const checkIn: CheckIn = {
                id: item['id'],
                content: item['content'],
                moods: item['moods'],
                createdAt: item['createdAt']
            };
            checkIns.push(checkIn);
        });
        
        return checkIns;
    } catch (error: any) {
        console.log(error, error.message);
    }
}

export const requestDeleteCheckIn = async (request: string) => {
    try {
        const response = await apiClient.post('/CheckIn/delete', {
            id: request
        });
        return response.data
    } catch (error: any){
        console.log(error, error.message);
    }
}

export const requestCreateCheckIn = async (request: CreateCheckInDTO) => {
    try {
        await apiClient.post('/CheckIn/create', request);
    } catch (error: any) {
        console.log(error, error.message);
    }
};

export const requestReports = async () => {
    try {
        const response = await apiClient.get('/Report');
        const reports: ReportData[] = [];
        response.data.forEach((item: any) => {
            const report: ReportData = {
                id: item['id'],
                reportName: item['reportName'],
                createdAt: item['createdAt']
            };
            reports.push(report);
        });
        return reports;
    } catch (error: any) {
        console.log(error, error.message);
    }
}

export const requestReportStream = async (reportId: string) => {
    try {    
        const response = await apiClient.post('/Report/stream', reportId, {responseType: 'blob'});
        return response.data;
    } catch (error: any) {
        console.log(error, error.message);
    }
}

export const requestUserInfo = async () => {
    try {
        const response = await apiClient.get('/User/info');
        const reportInfo: UserInfo = {
            email: response.data['email'],
            firstName: response.data['firstName']
        }
        return reportInfo;
    } catch (err: any) {
        console.log(err, err.message);
    }
}

export const requestReportUpdate = async (updatedReport: UpdatedReport) => {
    try {
        const response = await apiClient.post('/Report/update', updatedReport);
        return response.data;
    } catch (error: any) {
        console.log(error, error.message);
    }
}

export const requestReportDelete = async (report: DeleteReport) => {
    try {
        await apiClient.post('/Report/delete', report);
    } catch (err: any) {
        console.log(err.message || "Error deleting report");
    }
}

export const requestCreateReport = async (request: CreateReportRequest) => {
    try {
        await apiClient.post('/Report/generate', request);
    } catch (err: any) {
        console.log(err.message || "Error creating report");
    }
}

export const requestUpdateUserInfo = async (request: UserInfo) => {
    try {
        await apiClient.post('/User/update', request);
    } catch (err: any) {
        console.log(err.message || "Error updating user");
    }
}