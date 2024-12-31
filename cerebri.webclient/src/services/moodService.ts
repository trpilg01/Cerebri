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