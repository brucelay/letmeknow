import ComponentEdit from "./ComponentEdit";
import ComponentSelect from "./ComponentSelect";
import { createStore } from "solid-js/store";
import { createSignal } from "solid-js";
import { modules as modulesObject } from "./modules/modules";
import { unwrap } from "solid-js/store";

export default function WorkflowBuilder() {
  const [functions, setFunctions] = createStore([]);

  const [modules, setModules] = createSignal(modulesObject);

  const [minutes, setMinutes] = createSignal("?");
  const [repeat, setRepeat] = createSignal("?")

  function printWorkflow() {
    const unwrappedFunctions = unwrap(functions);
    console.log(JSON.stringify(unwrappedFunctions, null, 2));
  }

  async function runWorkflow() {
    const req = await fetch(
      "https://api.letmeknow.tech/Workflow/CreateWorkflow",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(functions, null, 2),
      }
    );

    console.log(req.status);
  }

  async function scheduleWorkflow() {
    setFunctions([{
        "function": "event",
        "options": {
            "timeinmins": minutes(),
            "repeats": repeat()
        }
    } ,...functions])

    const req = await fetch(
        "https://api.letmeknow.tech/Workflow/CreateEvent",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(functions, null, 2),
      }
    )
  }

  return (
    <>
      <div class="basis-8/12 p-5 bg-slate-200 dark:bg-slate-900 shadow-inner shadow-inner-lg">
        <div class="flex flex-row">
          <div class="basis-1/4 font-bold">
            <ComponentSelect
              modules={modules}
              setModules={setModules}
              functions={functions}
              setFunctions={setFunctions}
            />
          </div>
          <div class="basis-3/4">
            <ComponentEdit functions={functions} setFunctions={setFunctions} />
          </div>
        </div>
      </div>
      <div class="basis-2/12">
        <div class="flex text-2xl items-center h-full font-bold gap-x-5">
            <div class="flex justify-start ml-5">
                <button class="px-10 py-10 bg-gray-300 hover:bg-lime-500 rounded" onClick={runWorkflow}>Run Workflow</button>
            </div>
            <div class="flex justify-start">
                <button class="px-10 py-10 bg-gray-300 hover:bg-lime-500 rounded" onClick={scheduleWorkflow}>Schedule Workflow</button>
                <div class="flex flex-col items-start justify-around font-semibold ml-5">
                    <div class="">
                        Every <input onInput={(e) => setMinutes(e.target.value)} class="w-14 mx-3 text-center border rounded" type="text"></input> minutes
                    </div>
                    <div class="">
                        Repeat <input onInput={(e) => setRepeat(e.target.value)} class="w-14 mx-3 text-center border rounded" type="text"></input> times
                    </div>
                </div>
            </div>
        </div>
      </div>
    </>
  );
}
