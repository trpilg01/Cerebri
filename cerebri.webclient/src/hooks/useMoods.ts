import { Moods } from "data/dataTypes"
import { useEffect, useState } from "react"
import { getMoods } from "services";

const useMoods = () => {
    const [moods, setMoods] = useState<Moods | null>(null);

    const fetchMoods = async () => {
        try {
            const moods = await getMoods();
            setMoods(moods);
        } catch (err: any) {
            console.log(err.message ?? "Error fetching moods");
        }
    };

    useEffect(() => {
        fetchMoods();
    }, []);

    return { moods }
};

export default useMoods;