import { requestCheckIns, requestDeleteCheckIn, requestCreateCheckIn } from 'services';
import { CheckIn, CreateCheckInDTO } from '../data/dataTypes';
import { useEffect, useState } from "react";

const useCheckIns = () => {
    const [checkIns, setCheckins] = useState<CheckIn[] | undefined>(undefined);
    const [error, setError] = useState<string | null>(null);

    const fetchCheckIns = async () => {
        try {
            const checkIns = await requestCheckIns();
            setCheckins(checkIns);
        } catch (err: any) {
            setError(err.message ?? "Error fetching checkins");
        }
    };

    const deleteCheckIn = async (checkInId: string) => {
        try {
            await requestDeleteCheckIn(checkInId);
        } catch (err: any) {
            setError(err.message ?? "Error deleting CheckIn");
        }
    };

    const createCheckIn = async (request: CreateCheckInDTO) => {
        try {
            await requestCreateCheckIn(request);
        } catch (err: any) {
            setError(err.message ?? "Error creating CheckIn");
        }
    };

    useEffect(() => {
        fetchCheckIns();
    }, []);
    
    return {checkIns, deleteCheckIn, createCheckIn, error, setError}
};

export default useCheckIns;