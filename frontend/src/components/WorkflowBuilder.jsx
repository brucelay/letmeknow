import ComponentEdit from "./ComponentEdit";
import ComponentSelect from "./ComponentSelect";
import { createStore } from "solid-js/store";
import { createSignal } from "solid-js";
import { modules as modulesObject } from "./modules/modules";
import { unwrap } from "solid-js/store";

export default function WorkflowBuilder() {
  const [functions, setFunctions] = createStore([]);

  const [modules, setModules] = createSignal(modulesObject);

  function printWorkflow() {
    const unwrappedFunctions = unwrap(functions);
    unwrappedFunctions.map(func => delete func.component);
    console.log(JSON.stringify(unwrappedFunctions, null, 2));
  }

  return (
    <>
      <div class="basis-8/12 m-3">
        <div class="flex flex-row">
          <div class="basis-1/2 font-bold">
              <ComponentSelect
                  modules={modules}
                  setModules={setModules}
                  functions={functions}
                  setFunctions={setFunctions}
              />
          </div>
          <div class="basis-1/2">
            <ComponentEdit functions={functions} setFunctions={setFunctions}/>
          </div>
        </div>
      </div>
      <div class="basis-2/12 m-6">
        <button className="bg-gray-800 hover:bg-gray-600 text-white font-bold py-2 px-4 rounded" onClick={printWorkflow}>Run Workflow</button>
      </div>
    </>
  );

}

