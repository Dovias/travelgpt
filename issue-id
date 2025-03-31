import { useState } from "react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";

export default function MessageEditor() {
  const [message, setMessage] = useState("Čia jūsų išsiųstas pranešimas.");
  const [isEditing, setIsEditing] = useState(false);
  const [tempMessage, setTempMessage] = useState(message);

  const handleEdit = () => {
    setIsEditing(true);
    setTempMessage(message);
  };

  const handleSave = () => {
    setMessage(tempMessage);
    setIsEditing(false);
  };

  const handleCancel = () => {
    setIsEditing(false);
    setTempMessage(message);
  };

  return (
    <Card className="max-w-lg mx-auto p-4 mt-5">
      <CardContent>
        {isEditing ? (
          <Textarea
            value={tempMessage}
            onChange={(e) => setTempMessage(e.target.value)}
            className="w-full p-2 border rounded"
          />
        ) : (
          <p className="mb-2 text-gray-800">{message}</p>
        )}
        <div className="flex gap-2 mt-2">
          {isEditing ? (
            <>
              <Button onClick={handleSave} className="bg-blue-500 text-white">
                Išsaugoti
              </Button>
              <Button onClick={handleCancel} className="bg-gray-400 text-white">
                Atšaukti
              </Button>
            </>
          ) : (
            <Button onClick={handleEdit} className="bg-green-500 text-white">
              Redaguoti
            </Button>
          )}
        </div>
      </CardContent>
    </Card>
  );
}
