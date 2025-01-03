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

export interface CreateCheckIn {
    content: string
    moods: Mood[]
}