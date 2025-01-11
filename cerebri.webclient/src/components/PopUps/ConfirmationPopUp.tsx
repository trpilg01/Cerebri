interface ConfirmationPopUpProps {
    message: string;
    onConfirm: () => void;
    onCancel: () => void;
};

const ConfirmationPopUp:React.FC<ConfirmationPopUpProps> = ({ message, onConfirm, onCancel }) => (
    <div className="fixed flex flex-col inset-0 bg-black bg-opacity-50 items-center justify-center z-50">
        <div className="flex flex-col justify-center items-center font-bold h-fit w-fit p-5 flex-wrap rounded-md bg-bittersweet">
            <h1 className="mb-4">{message}</h1>
            <div className="flex flex-row gap-2">
                <button 
                    className="p-2 bg-green-200 rounded-md"
                    onClick={onConfirm}
                >
                    Confirm
                </button>
                <button 
                    className="p-2 bg-red-500 rounded-md"
                    onClick={onCancel}
                >
                    Cancel
                </button>
            </div>
        </div>
    </div>
);

export default ConfirmationPopUp;