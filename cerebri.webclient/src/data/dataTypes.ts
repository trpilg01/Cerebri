export interface Moods {
    highEnergyPositive: Mood[];
    highEnergyNegative: Mood[];
    lowEnergyPositive: Mood[];
    lowEnergyNegative: Mood[];
}

export interface Mood {
    id: string,
    name: string;
    moodType: number;
}

export interface LoginRequest {
    email: string;
    password: string;
}