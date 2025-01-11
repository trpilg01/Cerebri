import { requestMoods } from "services";
import { Mood, Moods } from "data/dataTypes";

export const getMoods = async () => {
    const response: Mood[] = await requestMoods() as Mood[];
    
    if (Array.isArray(response)){
        const highEnergyNegative: Mood[] = response.filter((mood: Mood) => mood.type === 0);
        const lowEnergyNegative: Mood[] = response.filter((mood: Mood) => mood.type === 1);
        const lowEnergyPositive: Mood[] = response.filter((mood: Mood) => mood.type === 2);
        const highEnergyPositive: Mood[] = response.filter((mood: Mood) => mood.type === 3);
    
        const moods: Moods = {
            highEnergyNegative: highEnergyNegative,
            highEnergyPositive: highEnergyPositive,
            lowEnergyNegative: lowEnergyNegative,
            lowEnergyPositive: lowEnergyPositive
        };
    
        return moods;
    }

    return null;
}

export const getColor = (moodType: number) => {
    switch(moodType) {
        case(0):
            return 'bg-red-200';
        case(1):
            return 'bg-blue-200';
        case(2):
            return 'bg-green-200';
        case(3):
            return 'bg-orange-200';
    }
}

export const getDateString = (date: Date) => {
    const validDate = new Date(date);
    return validDate.toLocaleDateString();
}

export const formatDate = (date: Date | null): string => {
    if (!date) return '';
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
};