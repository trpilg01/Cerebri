import { requestMoods } from "services";
import { Mood, Moods } from "data/dataTypes";

export const getMoods = async () => {
    const response: Mood[] = await requestMoods() as Mood[];
    
    if (Array.isArray(response)){
        const highEnergyNegative: Mood[] = response.filter((mood: Mood) => mood.moodType === 0);
        const lowEnergyNegative: Mood[] = response.filter((mood: Mood) => mood.moodType === 1);
        const lowEnergyPositive: Mood[] = response.filter((mood: Mood) => mood.moodType === 2);
        const highEnergyPositive: Mood[] = response.filter((mood: Mood) => mood.moodType === 3);
    
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
