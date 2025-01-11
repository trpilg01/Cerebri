export interface Moods {
    highEnergyPositive: Mood[];
    highEnergyNegative: Mood[];
    lowEnergyPositive: Mood[];
    lowEnergyNegative: Mood[];
}

export interface Mood {
    id: string
    name: string
    type: number
}

export interface LoginRequest {
    email: string
    password: string
}

export interface CreateJournalRequest {
    title: string
    content: string
    moods: Mood[]
}

export interface Journal {
    id: string
    title: string
    content: string
    moods: Mood[]
    createdAt: Date
}

export interface CheckIn {
    id: string
    content: string
    moods: Mood[]
    createdAt: Date
}

export interface CreateCheckInDTO {
    content: string
    moods: Mood[]
}

export interface ReportData {
    id: string
    reportName: string
    createdAt: Date
}

export interface UpdatedReport {
    id: string | undefined
    reportName: string | undefined
    createdAt: Date | undefined
}

export interface DeleteReport {
    reportId: string
}

export interface CreateReportRequest {
    startDate: Date | null
    endDate: Date | null
}

export interface UserInfo {
    email: string
    firstName: string
}